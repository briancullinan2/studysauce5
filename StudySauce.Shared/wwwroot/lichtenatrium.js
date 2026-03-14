export function initLichtenberg(canvasId) {
    const canvas = document.getElementById(canvasId);
    if (!canvas) return;
    const ctx = canvas.getContext('2d');
    let width, height;
    let isCancelled = false;

    const resize = () => {
        width = canvas.width = canvas.parentElement.clientWidth;
        height = canvas.height = canvas.parentElement.clientHeight;
    };
    window.addEventListener('resize', resize);
    resize();

    let time = 0;

    function project(x, y, z) {
        const factor = 500 / (z + 800);
        // Subtle floating shift
        const shiftX = Math.sin(time * 0.5) * 20;
        const shiftY = Math.cos(time * 0.5) * 15;
        return {
            x: (x * factor) + (width / 2) + shiftX,
            y: (y * factor) + (height / 2) + shiftY,
            scale: factor
        };
    }

    /**
     * Elemental Draw Logic
     * Types: 'stone', 'nature', 'electric', 'gold'
     */
    function drawElementalBranch(x1, y1, z1, x2, y2, z2, depth, type = 'electric') {
        if (depth <= 0) return;

        const p1 = project(x1, y1, z1);
        const p2 = project(x2, y2, z2);
        const pulse = (Math.sin(time * 2 + depth) + 1) / 2;

        ctx.beginPath();
        ctx.moveTo(p1.x, p1.y);
        ctx.lineTo(p2.x, p2.y);

        // Styling based on SVG definitions
        switch (type) {
            case 'stone':
                ctx.strokeStyle = `rgba(63, 98, 18, ${0.3 + pulse * 0.2})`; // atriumStone moss
                ctx.lineWidth = depth * 1.5;
                ctx.shadowBlur = 0;
                break;
            case 'nature':
                ctx.strokeStyle = `rgb(16, 185, 129)`; // natureBurst emerald
                ctx.lineWidth = depth * 0.8;
                ctx.shadowBlur = depth * 2;
                ctx.shadowColor = '#10b981';
                break;
            case 'electric':
                ctx.strokeStyle = `rgb(103, 232, 249)`; // naturalGlow cyan
                ctx.lineWidth = depth * 0.4 * pulse;
                ctx.shadowBlur = depth * 5 * pulse;
                ctx.shadowColor = '#67e8f9';
                break;
            case 'gold':
                ctx.strokeStyle = '#fbbf24'; // goldGrad
                ctx.lineWidth = 2;
                ctx.shadowBlur = 10;
                ctx.shadowColor = '#fbbf24';
                break;
        }

        ctx.stroke();

        // Recursion logic for 'electric' and 'nature' only
        if (type === 'electric' || type === 'nature') {
            const jitter = type === 'electric' ? 30 : 10;
            const midX = (x1 + x2) / 2 + (Math.random() - 0.5) * depth * jitter;
            const midY = (y1 + y2) / 2 + (Math.random() - 0.5) * depth * jitter;
            const midZ = (z1 + z2) / 2 + (Math.random() - 0.5) * depth * jitter;

            if (Math.random() > 0.2) {
                drawElementalBranch(x1, y1, z1, midX, midY, midZ, depth - 1, type);
                drawElementalBranch(midX, midY, midZ, x2, y2, z2, depth - 1, type);
            }
        }
    }

    function drawLocus(x, y, z, color) {
        const p = project(x, y, z);
        ctx.beginPath();
        ctx.arc(p.x, p.y, 4 * p.scale, 0, Math.PI * 2);
        ctx.fillStyle = color;
        ctx.shadowBlur = 15;
        ctx.shadowColor = color;
        ctx.fill();
    }

    function animate(currentTime) {
        if (isCancelled) return;
        time += 0.02;

        // Background trail (Secret Garden Ivory)
        ctx.fillStyle = 'rgba(252, 252, 240, 0.15)';
        ctx.fillRect(0, 0, width, height);

        // 1. ANCHOR NODES (Elementals)
        const anchors = [
            { pos: [-200, 200, 0], col: '#fbbf24', type: 'gold' },
            { pos: [200, 200, 0], col: '#fbbf24', type: 'gold' },
            { pos: [0, -250, 100], col: '#67e8f9', type: 'electric' },
            { pos: [150, 0, -100], col: '#fb7185', type: 'nature' }
        ];

        // 2. THE BIRDS (3 Orbitals)
        const birdCount = 3;
        for (let i = 0; i < birdCount; i++) {
            const angleOffset = i * (Math.PI * 2 / 3);
            const speed = time * (0.5 + i * 0.1);
            const r = 250 + Math.sin(time) * 50;

            const bx = Math.cos(speed + angleOffset) * r;
            const by = Math.sin(speed * 1.2) * 100;
            const bz = Math.sin(speed + angleOffset) * r;

            // Draw Bird Body
            drawElementalBranch(bx, by, bz, bx + 20, by - 10, bz, 2, 'gold');

            // 3. LICHTENBERG STRIKES to Anchors
            anchors.forEach(a => {
                const dx = bx - a.pos[0];
                const dy = by - a.pos[1];
                const dz = bz - a.pos[2];
                const dist = Math.sqrt(dx * dx + dy * dy + dz * dz);

                if (dist < 400) {
                    drawElementalBranch(bx, by, bz, a.pos[0], a.pos[1], a.pos[2], 4, i === 1 ? 'nature' : 'electric');
                }
            });
        }

        // 4. DRAW THE STRUCTURE (Stone Arch Base)
        const archNodes = [];
        for (let i = 0; i <= 10; i++) {
            const a = (i / 10) * Math.PI;
            archNodes.push([Math.cos(a) * 250, -Math.sin(a) * 250, 0]);
        }
        for (let i = 0; i < archNodes.length - 1; i++) {
            drawElementalBranch(...archNodes[i], ...archNodes[i + 1], 6, 'electric');
        }

        // 5. DRAW NODES
        anchors.forEach(a => drawLocus(...a.pos, a.col));

        requestAnimationFrame(animate);
    }

    animate(performance.now());
    return {
        dispose: () => {
            isCancelled = true;
            cancelAnimationFrame(requestId);
            window.removeEventListener('resize', resize);
        }
    };

}