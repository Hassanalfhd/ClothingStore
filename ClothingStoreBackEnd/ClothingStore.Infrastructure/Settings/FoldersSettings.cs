using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Endpoints;

namespace ClothingStore.Infrastructure.Settings
{
    public sealed class FoldersSettings
    {

        public string RootFolder { get; init; } = default!;
        public string ProductsFolder { get; init; } = default!;

    }
}
