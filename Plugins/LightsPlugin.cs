using AISample.Models;
using Microsoft.SemanticKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace AISample.Plugins
{
    public class LightsPlugin
    {
        private readonly List<LightModel> _lights = new()
        { 
            new LightModel { Id = 1, Name = "Table Lamp", Is_On = false },
            new LightModel { Id = 2, Name = "Porch light", Is_On = false },
            new LightModel { Id = 3, Name = "Chandelier", Is_On = true },
        };

        [KernelFunction("get_lights")]
        [Description("Get the list of lights and their states")]
        public async Task<List<LightModel>> GetLightsAsync()
        {
            return await Task.FromResult(_lights);
        }

        [KernelFunction("change_state")]
        [Description("Change the state of a light")]
        public async Task<LightModel?> GetStateAsync(int id, bool isOn)
        {
            var light = _lights.FirstOrDefault(x => x.Id == id);

            if (light is null)
            {
                return null;
            }
             light.Is_On = isOn;
            return light;
        }

        [KernelFunction("add_light")]
        [Description("Add a new light")]
        public async Task<LightModel?> AddLight(LightModel light)
        {
            if(light is null)
            {
                return null;
            }
            _lights.Add(light);
            return light;
        }
    }
}
