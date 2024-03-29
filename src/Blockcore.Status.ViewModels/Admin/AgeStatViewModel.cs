﻿using BlockcoreStatus.Entities.Admin;
using PersianUtils.Core;

namespace BlockcoreStatus.ViewModels.Admin;

public class AgeStatViewModel
{
    public const char RleChar = (char)0x202B;

    public int UsersCount { set; get; }
    public int AverageAge { set; get; }
    public User MaxAgeUser { set; get; }
    public User MinAgeUser { set; get; }

    public string MinMax =>
        Invariant(
            $"{RleChar} Youngest Member: {MinAgeUser.DisplayName} ({MinAgeUser.BirthDate.Value.GetAge()}), Oldest Member: {MaxAgeUser.DisplayName} ({MaxAgeUser.BirthDate.Value.GetAge()}), Among {UsersCount} people ");
}