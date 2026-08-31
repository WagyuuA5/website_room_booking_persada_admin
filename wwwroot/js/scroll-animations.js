// scroll-animations.js

// 1. Check for user preference (Reduced Motion)
const prefersReducedMotion = window.matchMedia('(prefers-reduced-motion: reduce)').matches;

let lenisInstance = null;
let rafId = null;

// Expose to Blazor
window.ScrollAnimations = {
    init: function (containerSelector) {
        if (prefersReducedMotion) return;

        const container = document.querySelector(containerSelector);
        if (!container) return;

        // Dynamic import Lenis from CDN if not bundled
        if (!window.Lenis) {
            const script = document.createElement('script');
            script.src = "https://cdn.jsdelivr.net/npm/lenis@1.1.18/dist/lenis.min.js";
            script.onload = () => {
                this._startLenis(container);
            };
            document.head.appendChild(script);
        } else {
            this._startLenis(container);
        }
    },

    _startLenis: function (container) {
        // Find wrapper and content. If container is the whole modal body, 
        // lenis needs the scrollable element.
        lenisInstance = new window.Lenis({
            wrapper: container,
            content: container.querySelector('.scroll-content-wrapper') || container.children[0],
            lerp: 0.1,
            duration: 1.2,
            orientation: 'vertical',
            gestureOrientation: 'vertical',
            smoothWheel: true,
            smoothTouch: false,
            touchMultiplier: 2,
        });

        // 2. Velocity Skew Effect
        const skewItems = container.querySelectorAll('.anim-velocity-skew');
        
        lenisInstance.on('scroll', (e) => {
            const velocity = e.velocity;
            // Cap velocity effect to subtle 2-3 degrees
            const maxSkew = 2.5;
            let skew = velocity * 0.1;
            if (skew > maxSkew) skew = maxSkew;
            if (skew < -maxSkew) skew = -maxSkew;

            skewItems.forEach(item => {
                item.style.transform = `skewY(${skew}deg)`;
            });
        });

        const sections = container.querySelectorAll('.anim-overlap-section');
        
        // Listen to scroll to adjust scale/opacity of the previous sections (Overlapping)
        lenisInstance.on('scroll', (e) => {
            sections.forEach((section, index) => {
                const rect = section.getBoundingClientRect();
                const containerRect = container.getBoundingClientRect();
                
                // If section is sticky at the top
                if (rect.top <= containerRect.top + 5) { // Threshold
                    // Calculate how much it is covered by the NEXT section
                    const nextSection = sections[index + 1];
                    if (nextSection) {
                        const nextRect = nextSection.getBoundingClientRect();
                        const distance = nextRect.top - containerRect.top;
                        const maxDistance = rect.height;
                        
                        if (distance < maxDistance && distance > 0) {
                            // Being covered
                            const progress = 1 - (distance / maxDistance);
                            // Scale down to 0.96, opacity down to 0.6
                            const scale = 1 - (progress * 0.04);
                            const opacity = 1 - (progress * 0.4);
                            
                            const inner = section.querySelector('.anim-overlap-inner');
                            if (inner) {
                                inner.style.transform = `scale(${scale})`;
                                inner.style.opacity = opacity;
                            }
                        } else if (distance <= 0) {
                            // Fully covered
                            const inner = section.querySelector('.anim-overlap-inner');
                            if (inner) {
                                inner.style.transform = `scale(0.96)`;
                                inner.style.opacity = 0.6;
                            }
                        }
                    }
                } else {
                    // Reset
                    const inner = section.querySelector('.anim-overlap-inner');
                    if (inner) {
                        inner.style.transform = `scale(1)`;
                        inner.style.opacity = 1;
                    }
                }
            });
        });

        function raf(time) {
            lenisInstance.raf(time);
            rafId = requestAnimationFrame(raf);
        }

        rafId = requestAnimationFrame(raf);
    },

    destroy: function () {
        if (lenisInstance) {
            lenisInstance.destroy();
            lenisInstance = null;
        }
        if (rafId) {
            cancelAnimationFrame(rafId);
            rafId = null;
        }
    }
};
