import { Injectable } from '@angular/core';

@Injectable()
export class Utilities {
    public static baseUrl() {
        let base = '';

        if (window.location.origin) {
            base = window.location.origin;
        } else {
            base =
                window.location.protocol +
                '//' +
                window.location.hostname +
                (window.location.port ? ':' + window.location.port : '');
        }

        return base.replace(/\/$/, '');
    }

    public static splitInTwo(
        text: string,
        separator: string
    ): { firstPart: string; secondPart: string } {
        const separatorIndex = text.indexOf(separator);

        if (separatorIndex === -1) {
            return { firstPart: text, secondPart: null };
        }

        const part1 = text.substr(0, separatorIndex).trim();
        const part2 = text.substr(separatorIndex + 1).trim();

        return { firstPart: part1, secondPart: part2 };
    }
}
