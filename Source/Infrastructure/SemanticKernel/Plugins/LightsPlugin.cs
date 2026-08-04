using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Domain.Models.AI;
using Microsoft.SemanticKernel;

namespace Infrastructure.SemanticKernel.Plugins
{
    public class LightsPlugin : IKernelPlugin
    {
        private readonly List<Lights> _lights = new()
        {
            new Lights { Id = 1, Name = "Living Room Light", IsOn = true },
            new Lights { Id = 2, Name = "Kitchen Light", IsOn = false },
            new Lights { Id = 3, Name = "Bedroom Light", IsOn = true }
        };

        [KernelFunction("get_lights")]
        [Description("Get the list of lights")]
        public List<Lights> Getlists() => _lights;


        [KernelFunction("change_light_state")]
        [Description("Change the state of a light")]
        public Lights? ChangeLightState(int id, bool isOn)
        { 
            var light = this._lights.FirstOrDefault(l => l.Id == id);
            if (light is null) return null;

            light.IsOn = isOn;
            return light;
        }
    }
}
