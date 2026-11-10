
class AudioTrack {
  constructor(language) { this.language = language; }
  play() { return `Воспроизводится аудиодорожка: [${this.language}]`; }
}

class Subtitles {
  constructor(language) { this.language = language; }
  show() { return `Отображаются субтитры: [${this.language}]`; }
}


class LanguageFactory {
  createAudio() { throw new Error("Метод не реализован"); }
  createSubtitles() { throw new Error("Метод не реализован"); }
}



export class RussianFactory extends LanguageFactory {
  createAudio() { return new AudioTrack("Русский"); }
  createSubtitles() { return new Subtitles("Русский"); }
}

export class EnglishFactory extends LanguageFactory {
  createAudio() { return new AudioTrack("English"); }
  createSubtitles() { return new Subtitles("English"); }
}

export class SpanishFactory extends LanguageFactory {
  createAudio() { return new AudioTrack("Español"); }
  createSubtitles() { return new Subtitles("Español"); }
}

export const getFactory = (lang) => {
  switch (lang) {
    case 'ru': return new RussianFactory();
    case 'en': return new EnglishFactory();
    case 'es': return new SpanishFactory();
    default: return new RussianFactory();
  }
};