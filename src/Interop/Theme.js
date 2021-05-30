
const darkThemeQuery = matchMedia('(prefers-color-scheme: dark)');

export function isDarkThemeActive() {
    return darkThemeQuery.matches;
}

export function registerThemeChangedCb(cb) {
    function isDarkTheme() {
        cb(darkThemeQuery.matches);
    }
    darkThemeQuery.addEventListener('change', isDarkTheme);
}

/**
 * 
 * @param {string?} theme
 */
export function overrideTheme(theme) {
    const body = document.body;
    if (!theme?.name?.toLowerCase() || theme?.name?.toLowerCase() === undefined || theme?.name?.toLowerCase() === 'undefined') {
        body.classList.remove('sl-theme-dark');
        return;
    }
    if (theme.name.toLowerCase() === 'dark') {
        body.classList.add('sl-theme-dark');
    } else {
        body.classList.remove('sl-theme-dark');
    }
}