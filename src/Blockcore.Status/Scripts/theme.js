const LOCAL_STORAGE_KEY = "theme";

const LOCAL_META_DATA = JSON.parse(localStorage.getItem(LOCAL_STORAGE_KEY));

const DARK_STYLE_LINK = document.getElementById("dark-theme-style");
const THEME_TOGGLER = document.getElementById("theme-toggler");

let isDark = LOCAL_META_DATA && LOCAL_META_DATA.isDark;

if (isDark) {
    enableDarkTheme();
} else {
    disableDarkTheme();
}

function toggleTheme() {
    isDark = !isDark;
    if (isDark) {
        enableDarkTheme();
    } else {
        disableDarkTheme();
    }
    const META = { isDark };
    localStorage.setItem(LOCAL_STORAGE_KEY, JSON.stringify(META));
}

function enableDarkTheme() {
    document.getElementsByTagName('body')[0].setAttribute('class', 'bootstrap-dark');
    THEME_TOGGLER.innerHTML = "<i class='fas fa-sun'></i> Light";
}

function disableDarkTheme() {

    document.getElementsByTagName('body')[0].setAttribute('class', 'bootstrap bg-image');
    THEME_TOGGLER.innerHTML = "<i class='fas fa-moon'></i> Dark";
}
