﻿namespace BlockcoreStatus.ViewModels.Admin.Settings;

public class Connectionstrings
{
    public SqlServer SqlServer { get; set; }
    public Localdb LocalDb { get; set; }
    public SQLite SQLite { get; set; }
}