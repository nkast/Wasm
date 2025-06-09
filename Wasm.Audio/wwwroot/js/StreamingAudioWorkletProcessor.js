class StreamingAudioWorkletProcessor extends AudioWorkletProcessor {

    constructor(options) {
        super();

        this.uid = -1;
        this.keepAlive = true;
        this.buffers = [];
        this.currentBuffer = [];
        this.bufferIndex = 0;
        this.isEmpty = true;

        this.phase = 0;

        this.port.onmessage = event => {
            if (event.data.type === "s") {
                this.buffers.push(new Float32Array(event.data.buffer));
                this.isEmpty = false;
                return;
            }
            if (event.data.type === "c") {
                this.buffers = [];
                this.isEmpty = true;
                return;
            }
            if (event.data.type === "q") {
                this.buffers = [];
                this.currentBuffer = [];
                this.bufferIndex = 0;
                this.isEmpty = true;
                this.keepAlive = false;
                return;
            }
            if (event.data.type === "uid") {
                this.uid = event.data.uid;
                return;
            }
        }

        this.port.onmessageerror = e => console.error(`processor ERROR > ${e.data}`, e);
    }

    tryDequeueBuffer() {
        const wasEmpty = this.isEmpty;
        this.isEmpty = this.buffers.length == 0;

        if (this.isEmpty && !wasEmpty) {
            this.currentBuffer = [];

            console.warn('No buffer to process.');
        }

        if (!this.isEmpty) {
            this.currentBuffer = this.buffers.shift();
            this.bufferIndex = 0;
            this.port.postMessage({ type: 'dq', remaining: this.buffers.length, uid: this.uid });

            return true;
        }

        return false;
    }

    process(inputs, outputs, parameters) {
        if (this.uid == -1) {
            return this.keepAlive;
        }

        if (this.currentBuffer.length == 0 && !this.tryDequeueBuffer()) {
            return this.keepAlive;
        }

        const sampleCount = outputs[0][0].length;
        const channelCount = outputs[0].length;

        for (let sampleIndex = 0; sampleIndex < sampleCount; sampleIndex++) {
            for (let channelIndex = 0; channelIndex < channelCount; channelIndex++) {

                if (this.bufferIndex >= this.currentBuffer.length && !this.tryDequeueBuffer()) {
                    return this.keepAlive;
                }

                const channelSample = this.currentBuffer[this.bufferIndex++];

                for (let outputIndex = 0; outputIndex < outputs.length; outputIndex++) {
                    outputs[outputIndex][channelIndex][sampleIndex] = channelSample;
                }
            }
        }

        return this.keepAlive;
    }
}

registerProcessor("streaming-audio-worklet", StreamingAudioWorkletProcessor);