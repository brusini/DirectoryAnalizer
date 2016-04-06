﻿namespace Directory_Analizer.Entities
{
    // используется для маппинга прав из 'Generic' в удобочитаемые
    public enum GenericRights : uint
    {
        GenericRead = 0x80000000,
        GenericWrite = 0x40000000,
        GenericExecute = 0x20000000,
        GenericAll = 0x10000000
    }
}
