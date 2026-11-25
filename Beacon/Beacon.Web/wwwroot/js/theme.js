window.beaconTheme = {
    get: () => document.documentElement.dataset.theme || "nuit",
    set: (theme) => {
        document.documentElement.dataset.theme = theme;
        try { localStorage.setItem("beacon-theme", theme); } catch { }
    }
};