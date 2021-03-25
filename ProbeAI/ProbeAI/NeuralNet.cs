using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProbeAI
{
    public class NeuralNet
    {
        int iNodes, hNodes, oNodes, hLayers;
        Matrix[] weights;
        public NeuralNet(int input, int hidden, int output, int hiddenLayers)
        {
            iNodes = input;     //8
            hNodes = hidden;    //6
            oNodes = output;    //4
            hLayers = hiddenLayers; //1

            weights = new Matrix[hLayers + 1];
            weights[0] = new Matrix(hNodes, iNodes + 1);
            for (int i = 1; i < hLayers; i++)
            {
                weights[i] = new Matrix(hNodes, hNodes + 1);
            }
            weights[weights.Length - 1] = new Matrix(oNodes, hNodes + 1);
            for (int i = 0; i < weights.Length; i++)
            {
                weights[i].randomize();
            }
        }
        public double[] output(double[] inputsArr)
        {
            Matrix inputs = weights[0].singleColumnMatrixFromArray(inputsArr);
            Matrix curr_bias = inputs.addBias();
            for (int i = 0; i < hLayers; i++)
            {
                Matrix hidden_ip = weights[i].dot(curr_bias);
                Matrix hidden_op = hidden_ip.activate();
                curr_bias = hidden_op.addBias();
            }
            Matrix output_ip = weights[weights.Length - 1].dot(curr_bias);
            Matrix output = output_ip.activate();
            for (int i = 0; i < output.matrix.Length; i++)
            {
                output.matrix[i, 0] = Character.Sigmoid(output.matrix[i, 0]);
                output.matrix[i, 0] = (output.matrix[i, 0] <= 0.5) ? 0 : output.matrix[i, 0];
            }
            return output.toArray();
        }
    }
}
