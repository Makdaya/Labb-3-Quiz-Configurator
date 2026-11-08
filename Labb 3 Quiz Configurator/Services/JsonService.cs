
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Labb_3_Quiz_Configurator.Models;

namespace Labb_3_Quiz_Configurator.Services
{
    class JsonService
    {

        public async Task SaveAsync(List<QuestionPack> packs, string filePath)
        {
            await using FileStream stream = new FileStream(filePath, FileMode.Create);

            await JsonSerializer.SerializeAsync(
                stream,
                packs,
                new JsonSerializerOptions { WriteIndented = true });
        }

        public async Task<List<QuestionPack>> LoadAsync(string filePath)
        {
            await using FileStream stream = new FileStream(filePath, FileMode.Open);

            return await JsonSerializer.DeserializeAsync<List<QuestionPack>>(stream)
                   ?? new List<QuestionPack>();
        }
    }
}
