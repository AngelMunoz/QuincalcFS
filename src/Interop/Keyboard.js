import keyboardjs from 'keyboardjs';

export function bindToContext(bindings = [], ctx = 'global') {
    keyboardjs.setContext(ctx);
    for (const [keySeqyence, callback] of bindings) {
        keyboardjs.bind(keySeqyence, callback, null, true);
    }
    keyboardjs.setContext('global');
}

export function bindSingleToGlobal(keyboardSequence, callback) {
    const binding = [keyboardSequence, callback];
    bindToContext([binding]);
}

export function start() {
    keyboardjs.watch();
}

export function stop() {
    keyboardjs.stop();
}

export function setContext(context = 'global') {
    keyboardjs.setContext(context);
}