using Newtonsoft.Json;
using SpotifyAPI.Web.Models;
using System.Collections.Generic;
using splaylist.Helpers;

namespace splaylist.Models
{
    public class ListingPlaylist
    {

        // Workaround for selecting in DataGrid
        [JsonConstructor]
        public ListingPlaylist(string id)
        {
            Id = id;
        }


        public ListingPlaylist(SimplePlaylist sp)
        {
            Playlist = sp;
            Id = sp.Id;
            TrackCount = sp?.Tracks?.Total;
        }

        public ListingPlaylist(FullPlaylist fp)
        {
            Playlist = fp;
            Id = fp.Id;
            TrackCount = fp?.Tracks?.Total;
        }


        /// <summary>
        /// for special playlists
        /// </summary>
        /// <param name="tracks"></param>
        /// <param name="name"></param>
        public ListingPlaylist(List<ListingTrack> tracks, string name)
        {
            Playlist = new SimplePlaylist();
            Playlist.Name = name;
            Tracks = tracks;
            TrackCount = tracks.Count;
        }

        [JsonProperty]
        public string Id { get; protected set; }

        [JsonIgnore]
        private Playlist Playlist { get; set; }

        public string Name => Playlist?.Name;

        public string Owner => Playlist?.Owner.DisplayName;

        public int? TrackCount { get; private set; }

        public string Collaborative => Playlist?.Collaborative.ToString();

        // TODO - other people's public playlists show the following as false
        public string Public => Playlist?.Public.ToString();


        [JsonIgnore] // as I imagine this field could cause memory issues if it's loaded in the main playlist page
        public List<ListingTrack> Tracks;

        public bool IsLoaded;


    }
}
