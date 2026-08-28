window.applyThemeMode = function (isDark) {
    if (isDark) document.body.classList.add('dark');
    else document.body.classList.remove('dark');
};

window.initSidebarDrag = function (dotNetRef) {
    var layout = document.querySelector('.admin-layout');
    if (!layout) return;
    var sidebar = layout.querySelector('.admin-sidebar');
    if (!sidebar) return;

    var startX = 0, startY = 0, dragging = false, THRESHOLD = 40;

    sidebar.addEventListener('pointerdown', function (e) {
        var rect = sidebar.getBoundingClientRect();
        if (e.clientX < rect.right - 18) return;
        if (e.pointerType === 'mouse' && e.button !== 0) return;
        startX = e.clientX;
        startY = e.clientY;
        dragging = true;
    });

    window.addEventListener('pointermove', function (e) {
        if (!dragging) return;
        var dx = e.clientX - startX;
        var dy = e.clientY - startY;
        if (Math.abs(dx) > THRESHOLD && Math.abs(dx) > Math.abs(dy)) {
            dragging = false;
            var collapsed = layout.classList.contains('sidebar-collapsed');
            var shouldCollapse = dx < 0;
            if (shouldCollapse !== collapsed) {
                dotNetRef.invokeMethodAsync('ToggleSidebarFromJs');
            }
        }
    });

    window.addEventListener('pointerup', function () { dragging = false; });
};

window.downloadBase64File = function (fileName, base64, mimeType) {
    var byteChars = atob(base64);
    var byteNumbers = new Array(byteChars.length);
    for (var i = 0; i < byteChars.length; i++) {
        byteNumbers[i] = byteChars.charCodeAt(i);
    }
    var byteArray = new Uint8Array(byteNumbers);
    var blob = new Blob([byteArray], { type: mimeType });
    var url = URL.createObjectURL(blob);
    var a = document.createElement('a');
    a.href = url;
    a.download = fileName;
    document.body.appendChild(a);
    a.click();
    document.body.removeChild(a);
    URL.revokeObjectURL(url);
};
