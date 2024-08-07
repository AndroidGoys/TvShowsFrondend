﻿using DamnTv.Api.Client.Models;

using Microsoft.VisualBasic;

namespace DamnTv.Api.Client.Entities;

public record TvChannelRelease
{
    public required int ShowId { get; init; }
    public required string ShowName { get; init; }
    public required float ShowAssessment { get; init; }
    public required AgeLimit ShowAgeLimit { get; init; }

    public string? PreviewUrl { get; init; }
    public string? Description { get; init; }
    public DateTimeOffset TimeStart { get; init; }
    public DateTimeOffset TimeStop { get; init; }

    internal static TvChannelRelease FromModel(TvChannelReleaseModel model, TimeSpan timeZone)
    {
        DateTimeOffset timeStart = DateTimeOffset.FromUnixTimeSeconds(model.TimeStart);
        DateTimeOffset timeStop = DateTimeOffset.FromUnixTimeSeconds(model.TimeStop);
        return new TvChannelRelease()
        {
            ShowId = model.ShowId,
            ShowName = model.ShowName,
            ShowAssessment = model.ShowAssessment,
            ShowAgeLimit = model.ShowAgeLimit,
            PreviewUrl = model.PreviewUrl,
            Description = model.Description,
            TimeStart = new(timeStart.DateTime + timeZone, timeZone),
            TimeStop = new(timeStop.DateTime + timeZone, timeZone)
        };
    }

    public override string ToString()
    {
        return
            $"({TimeStart.Hour:d2}:{TimeStart.Minute:d2} - " +
            $"{TimeStop.Hour:d2}:{TimeStop.Minute:d2})" +
            $" {ShowName}";
    }
}
