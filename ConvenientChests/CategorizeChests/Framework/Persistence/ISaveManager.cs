﻿namespace ConvenientChests.Framework.CategorizeChests.Framework.Persistence
{
    interface ISaveManager
    {
        void Save(string relativePath);
        void Load(string relativePath);
    }
}