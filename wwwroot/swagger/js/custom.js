(function () {
    document.title = "Developer API Reference | Fenix Alliance Group";
    var link = document.querySelector("link[rel*='icon']") || document.createElement('link');
    document.head.removeChild(link);
    link = document.querySelector("link[rel*='icon']") || document.createElement('link');
    document.head.removeChild(link);
    link = document.createElement('link');
    link.type = 'image/x-icon';
    link.rel = 'shortcut icon';
    link.href = 'https://fenixalliance.com.co/images/FenixAlliance/AppIcon/apple-icon-120x120.png';
    document.getElementsByTagName('head')[0].appendChild(link);
})();