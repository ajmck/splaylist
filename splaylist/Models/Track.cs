﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpotifyAPI.Web.Models;
using splaylist.Helpers;

namespace splaylist.Models
{
 /// <summary>
 ///  Track used in listings of tracks.
 ///
 /// While ideally this would inherit from FullTrack and just add extra methods, downcasting proved too cumbersome.
 /// </summary>
    public class Track
 {


        public FullTrack OriginalTrack { get; protected set; }

        public Track(FullTrack fullTrack)
        {
            OriginalTrack = fullTrack;
            Artists = CreateArtistString(fullTrack);

            
        }


        public string TrackTitle => OriginalTrack.Name;
        public string Album => OriginalTrack.Album.Name;

        public string Artists { get; protected set; }


        // todo - fix for precision
        public string AlbumDate => OriginalTrack.Album.ReleaseDate;

        

        /// Because FullArtist has fewer extra fields than FullArtist, prefer FullArtist for Genre info.
        /// OTOH, a track will only have one album to get genre from, but multiple artists.

        public FullArtist FullArtist { get; protected set; }

        private FullAlbum _fullAlbum;

        public FullAlbum FullAlbum
        {
            get
            {
                // if the fullAlbum has already been set for this track, return it
                if (_fullAlbum != null) return _fullAlbum;

                // else, see if the full album is in the cache
                FullAlbum cacheresult;
                if (Cache.FullAlbums.TryGetValue(OriginalTrack.Album.Id, out cacheresult))
                {
                    _fullAlbum = cacheresult;
                    return _fullAlbum;
                }

                return null;
            }
        }

        public List<string> Genres;

        public string GenreString { get; protected set; }


        public static string CreateArtistString(FullTrack ft)
        {
            string result = ft?.Artists[0]?.Name;

            // if more than one artist, add seperator
            for (int i = 1; i < ft?.Artists.Count; i++)
            {
                result += "; " + ft?.Artists[i].Name;
            }

            return result;
        }

        public static string CreateGenreString(FullArtist fa)
        {
            if (fa == null) return "";
            if (fa.Genres.Count == 0) return "";

            string result = fa?.Genres[0];
            for (int i = 1; i < fa?.Genres.Count; i++)
            {
                result += "; " + fa?.Genres[i];
            }

            return result;
        }

        public async void LoadFullArtist()
        {
            // throw new NotImplementedException();
            FullArtist = await API.S.GetArtistAsync(OriginalTrack.Artists[0].Id);
            Genres = FullArtist.Genres;
            GenreString = CreateGenreString(FullArtist);
        }
    
    }
}
