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
    var logo = sidebar.querySelector('.sidebar-logo');
    var header = sidebar.querySelector('.sidebar-header');

    var startX = 0, startY = 0, dragging = false, THRESHOLD = 35;
    var touchStartX = 0, touchStartY = 0;

    // Double click / Double tap on logo or header or handle
    function onDblClick() {
        dotNetRef.invokeMethodAsync('ToggleSidebarFromJs');
    }

    if (handle) handle.addEventListener('dblclick', onDblClick);
    if (header) header.addEventListener('dblclick', onDblClick);

    // Pointer Drag Handle
    function onPointerDown(e) {
        var rect = sidebar.getBoundingClientRect();
        if (e.target === handle || (e.clientX >= rect.right - 14 && e.clientX <= rect.right + 6)) {
            if (e.pointerType === 'mouse' && e.button !== 0) return;
            startX = e.clientX;
            startY = e.clientY;
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

    // Touch Swipe Gesture for Tablet/Mobile
    sidebar.addEventListener('touchstart', function (e) {
        if (e.touches.length === 1) {
            touchStartX = e.touches[0].clientX;
            touchStartY = e.touches[0].clientY;
        }
    }, { passive: true });

    sidebar.addEventListener('touchend', function (e) {
        if (e.changedTouches.length === 1) {
            var dx = e.changedTouches[0].clientX - touchStartX;
            var dy = e.changedTouches[0].clientY - touchStartY;
            if (Math.abs(dx) > 50 && Math.abs(dx) > Math.abs(dy) * 1.5) {
                var collapsed = layout.classList.contains('sidebar-collapsed');
                if (dx < 0 && !collapsed) {
                    dotNetRef.invokeMethodAsync('ToggleSidebarFromJs');
                } else if (dx > 0 && collapsed) {
                    dotNetRef.invokeMethodAsync('ToggleSidebarFromJs');
                }
            }
        }
    }, { passive: true });

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
