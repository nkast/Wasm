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
        var inChannel0 = inputs[0][0];
        if (inChannel0)
        {
            // convert to 16-6bit PCM
            var int16 = new Int16Array(inChannel0.length);
            for (var i = 0; i < inChannel0.length; i++)
            {
                var s = Math.max(-1, Math.min(1, inChannel0[i]));
                int16[i] = s * 32767;
            }

            var byteArray = new Uint8Array(int16.buffer);
            this.port.postMessage(byteArray, [byteArray.buffer]);
        }

        return true;
    }
}

registerProcessor('mic-processor', MicProcessor);