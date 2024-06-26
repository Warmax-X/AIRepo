﻿using AITest.Models.Enums;

namespace AITest.Models.Layers;

public class HiddenLayer(int numberOfNeurons, int numberOfPreviousNeurons, NeuronType type, string typeString)
    : Layer(numberOfNeurons, numberOfPreviousNeurons, type, typeString)
{
    public override void Recognize(Layer? nextLayer = null, NeuralNetwork? network = null)
    {
        var hiddenOut = new decimal[Neurons.Length];
        
        for (var i = 0; i < Neurons.Length; ++i)
            hiddenOut[i] = Neurons[i].Output;
        
        if (nextLayer != null) nextLayer.Data = hiddenOut;
    }

    public override decimal[]? BackwardPass(decimal[] gradientsSums)
    {
        decimal[]? gradientSum = null;

        for (var i = 0; i < NumberOfNeurons; ++i)
        for (var n = 0; n < NumberOfPreviousNeurons; ++n)
        {
            var inputs = Neurons[i].Inputs;
            if (inputs != null)
                Neurons[i].Weights[n] += LearningRate * inputs[n] * Neurons[i]
                    .CalculateGradient(0, Neurons[i].CalculateDerivative(Neurons[i].Output), gradientsSums[i]);
        }

        return gradientSum;
    }
}
