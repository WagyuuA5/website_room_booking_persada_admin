window.applyThemeMode = function (isDark) {
    if (isDark) {
        document.body.classList.add('dark');
        document.documentElement.classList.add('dark');
    } else {
        document.body.classList.remove('dark');
        document.documentElement.classList.remove('dark');
    }
};

window.initSidebarDrag = function (dotNetRef) {
    var layout = document.querySelector('.admin-layout');
    if (!layout) return;
    var sidebar = layout.querySelector('.admin-sidebar');
    if (!sidebar) return;
    var handle = sidebar.querySelector('.sidebar-drag-handle');

    var startX = 0, dragging = false, THRESHOLD = 35;

    function onPointerDown(e) {
        var rect = sidebar.getBoundingClientRect();
        // Trigger if clicking near edge or on handle
        if (e.target === handle || (e.clientX >= rect.right - 14 && e.clientX <= rect.right + 6)) {
            if (e.pointerType === 'mouse' && e.button !== 0) return;
            startX = e.clientX;
            dragging = true;
            sidebar.classList.add('dragging');
            if (handle) handle.classList.add('active');
            try { e.target.setPointerCapture(e.pointerId); } catch(err){}
        }
    }

    function onPointerMove(e) {
        if (!dragging) return;
        var dx = e.clientX - startX;
        if (Math.abs(dx) > THRESHOLD) {
            var collapsed = layout.classList.contains('sidebar-collapsed');
            var shouldCollapse = dx < 0;
            if (shouldCollapse !== collapsed) {
                dotNetRef.invokeMethodAsync('ToggleSidebarFromJs');
                dragging = false;
                sidebar.classList.remove('dragging');
                if (handle) handle.classList.remove('active');
            }
        }
    }

    function onPointerUp(e) {
        if (dragging) {
            dragging = false;
            sidebar.classList.remove('dragging');
            if (handle) handle.classList.remove('active');
            try { e.target.releasePointerCapture(e.pointerId); } catch(err){}
        }
    }

    sidebar.addEventListener('pointerdown', onPointerDown);
    window.addEventListener('pointermove', onPointerMove);
    window.addEventListener('pointerup', onPointerUp);
    window.addEventListener('pointercancel', onPointerUp);
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
