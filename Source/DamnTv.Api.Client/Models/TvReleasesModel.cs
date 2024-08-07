﻿using System.Text.Json.Serialization;

namespace DamnTv.Api.Client.Models
{
    internal record TvReleasesModel
    {
        [JsonPropertyName("timeStart")]
        public required long TimeStart { get; init; }

        [JsonPropertyName("timeStop")]
        public required long TimeStop { get; init; }

        [JsonPropertyName("totalCount")]
        public required int TotalCount { get; init; }

        [JsonPropertyName("releases")]
        public required IEnumerable<TvChannelReleaseModel> Releases { get; init; }
    }
}