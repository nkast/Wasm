//   streamProcessor.js
class StreamProcessor extends AudioWorkletProcessor
{
    static get parameterDescriptors()
    {
        return [
            {
                name: 'InputChannelCount',
                defaultValue: 2,
                minValue: 1,
                maxValue: 2,
                automationRate: 'k-rate'
            },
            {
                name: 'InputSampleRate',
                defaultValue: 44100,
                minValue:  8000,
                maxValue: 96000,
                automationRate: 'k-rate'
            }
        ];
    }

    constructor()
    {
        super();
        this.queue = [];

        this.port.onmessage = (event) =>
        {
            var data = event.data;

            if (typeof data === 'number')
            {
                //this.port.postMessage(data); // echo back test
            }
            if (data instanceof Uint8Array)
            {
                const buffer = new Int16Array(data.buffer, data.byteOffset, data.length / 2);
                this.queue.push(buffer);
            }
        };
    }


    process(inputs, outputs, parameters)
    {
        const inChannelCount = parameters.InputChannelCount;
        const inSampleRate     = parameters.InputSampleRate;

        const output = outputs[0];

        const outChannelCount = output.length;
        const outSampleCount = output[0].length;

        for (let c = 0; c < outChannelCount; c++)
        {
            const channel = output[c];

            if (this.queue.length > 0)
            {
                const buffer = this.queue.shift();
                for (let i = 0; i < channel.length && i < buffer.length; i++)
                {
                    let value = (buffer[i] / 32767);
                    channel[i] = value;
                }
            }
        };

        return true;
    }
}

registerProcessor("stream-processor", StreamProcessor);