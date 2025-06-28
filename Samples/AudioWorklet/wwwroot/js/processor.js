//   processor.js
class RandomNoiseProcessor extends AudioWorkletProcessor
{
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
        const output = outputs[0];
        output.forEach((channel) =>
        {
            if (this.queue.length > 0)
            {
                const buffer = this.queue.shift();
                for (let i = 0; i < channel.length && i < buffer.length; i++)
                {
                    var value = (buffer[i] / 32767);
                    channel[i] = value;
                }
            }
        });
        return true;
    }
}

registerProcessor("random-noise-processor", RandomNoiseProcessor);