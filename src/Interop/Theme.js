
const darkThemeQuery = matchMedia('(prefers-color-scheme: dark)');

export function isDarkThemeActive() {
    return darkThemeQuery.matches;
}

export function registerThemeChangedCb(cb) {
    function isDarkTheme() {
        cb(darkThemeQuery.matches)
    }
    darkThemeQuery.addEventListener('change', isDarkTheme);
}

/**
 * 
 * @param {string?} theme
 */
export function overrideTheme(theme) {
    const html = document.querySelector('html');
    if (!theme?.name?.toLowerCase() || theme?.name?.toLowerCase() === undefined || theme?.name?.toLowerCase() === 'undefined') {
        html?.removeAttribute('data-theme');
        return;
    }
    html?.setAttribute('data-theme', theme?.name?.toLowerCase())
}