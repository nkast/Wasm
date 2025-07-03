//   micProcessor.js
class MicProcessor extends AudioWorkletProcessor
{
    constructor()
    {
        super();

        // global variables for testing
        var sampleRate = globalThis.sampleRate;
        var currentFrame = globalThis.currentFrame;
        var currentTime = globalThis.currentTime;
        var currentRenderQuantum = globalThis.currentRenderQuantum;

        this.port.onmessage = (event) =>
        {
            var data = event.data;

            if (typeof data === 'number')
            {
                //this.port.postMessage(data); // echo back test
            }
            if (data instanceof Uint8Array)
            {
            }
        };
    }

    process(inputs, outputs, parameters)
    {
        const input = inputs[0];

        const inChannelCount = input.length;
        const inSampleCount = input[0].length;

        const inChannel0 = input[0];
        if (inChannel0)
        {
            // convert to 16-6bit PCM
            var int16 = new Int16Array(inSampleCount);
            for (var i = 0; i < inSampleCount; i++)
            {
                let value = (inChannel0[i] * 32767);
                int16[i] = value;
            }

            var byteArray = new Uint8Array(int16.buffer);
            this.port.postMessage(byteArray, [byteArray.buffer]);
        }

        return true;
    }
}

registerProcessor('mic-processor', MicProcessor);