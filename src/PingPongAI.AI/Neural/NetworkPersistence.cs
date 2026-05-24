using PingPongAI.AI.Neural.Activations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace PingPongAI.AI.Neural
{
    public static class NetworkPersistence
    {
        public static void Save(NeuralNetwork network, string path)
        {
#if DEBUG
            if (network == null)
                throw new ArgumentNullException(nameof(network));

            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException(nameof(path));
#endif
            NetworkDto dto = new NetworkDto
            {
                InputCount = network.InputCount,
                Layers = new List<LayerDto>(network.Layers.Count)
            };

            foreach (Layer layer in network.Layers)
            {
                LayerDto layerDto = new LayerDto
                {
                    Activation = layer.Neurons[0].Activation.GetType().Name,
                    Neurons = new List<NeuronDto>(layer.Neurons.Count)
                };

                foreach (Neuron neuron in layer.Neurons)
                {
                    double[] weights = new double[neuron.Weights.Length];
                    Array.Copy(neuron.Weights, weights, neuron.Weights.Length);

                    layerDto.Neurons.Add(new NeuronDto
                    {
                        Weights = weights,
                        Bias = neuron.Bias
                    });
                }

                dto.Layers.Add(layerDto);
            }

            JsonSerializerOptions options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            string json = JsonSerializer.Serialize(dto, options);

            string directory = Path.GetDirectoryName(path);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            File.WriteAllText(path, json);
        }

        public static NeuralNetwork Load(string path)
        {
#if DEBUG
            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException(nameof(path));

            if (!File.Exists(path))
                throw new FileNotFoundException("Network file not found.", path);
#endif
            string json = File.ReadAllText(path);
            NetworkDto dto = JsonSerializer.Deserialize<NetworkDto>(json);

            if (dto == null)
                throw new InvalidOperationException($"Failed to deserialize network from '{path}'.");

            NeuralNetwork network = new NeuralNetwork(dto.InputCount);

            foreach (LayerDto layerDto in dto.Layers)
            {
                IActivationFunction activation = ActivationFactory.Create(layerDto.Activation);
                network.AddLayer(layerDto.Neurons.Count, activation);

                Layer layer = network.Layers[network.Layers.Count - 1];
                for (int n = 0; n < layerDto.Neurons.Count; n++)
                {
                    NeuronDto neuronDto = layerDto.Neurons[n];
                    layer.Neurons[n].LoadParameters(neuronDto.Weights, neuronDto.Bias);
                }
            }

            return network;
        }
    }

    public sealed class NetworkDto
    {
        public int InputCount { get; set; }
        public List<LayerDto> Layers { get; set; }
    }

    public sealed class LayerDto
    {
        public string Activation { get; set; }
        public List<NeuronDto> Neurons { get; set; }
    }

    public sealed class NeuronDto
    {
        public double[] Weights { get; set; }
        public double Bias { get; set; }
    }

    internal static class ActivationFactory
    {
        public static IActivationFunction Create(string name)
        {
            switch (name)
            {
                case nameof(TanhActivation):
                    return new TanhActivation();
                case nameof(SigmoidActivation):
                    return new SigmoidActivation();
                case nameof(ReLUActivation):
                    return new ReLUActivation();
                case nameof(IdentityActivation):
                    return new IdentityActivation();
                default:
                    throw new InvalidOperationException($"Unknown activation: '{name}'.");
            }
        }
    }
}
