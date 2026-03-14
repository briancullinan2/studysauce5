export function initLichtenberg(canvasId) {
    const canvas = document.getElementById(canvasId);
    const ctx = canvas.getContext('2d');
    let width, height;

    const resize = () => {
        width = canvas.width = canvas.parentElement.clientWidth;
        height = canvas.height = canvas.parentElement.clientHeight;
    };
    window.addEventListener('resize', resize);
    resize();

    let angle = 0;

    function project(x, y, z) {
        const cos = Math.cos(angle * 0.5);
        const sin = Math.sin(angle * 0.5);
        let nx = x * cos - z * sin;
        let nz = x * sin + z * cos;
        let ny = y * cos - nz * sin;
        nz = y * sin + nz * cos;

        const factor = 400 / (nz + 600);
        return { x: nx * factor + width / 2, y: ny * factor + height / 2, nz: nz };
    }

    function drawBranch(x1, y1, z1, x2, y2, z2, depth, jitterScale = 10) {
        if (depth <= 0) return;

        const p1 = project(x1, y1, z1);
        const p2 = project(x2, y2, z2);

        // Create a pulse value (0 to 1) based on time and depth
        const pulse = (Math.cos(angle + (depth * 0.3)) + 1) / 2;

        // --- UPDATED COLOR LOGIC ---
        // Shifted from electric to warm/organic tones that evoke sunlight and plants
        let color;
        const colorSeed = (pulse + Math.random() * 0.2);

        if (colorSeed > 0.8) {
            // High pulse: Warm Golden/Amber (Sunlight burst)
            const r = Math.floor(220 + pulse * 35);
            const g = Math.floor(180 + pulse * 45);
            const b = Math.floor(100 + pulse * 50);
            color = `rgb(${r}, ${g}, ${b})`;
        } else if (colorSeed > 0.4) {
            // Mid pulse: Sage Green (Indoor plant life)
            const r = Math.floor(120 + pulse * 40);
            const g = Math.floor(160 + pulse * 30);
            const b = Math.floor(110 + pulse * 20);
            color = `rgb(${r}, ${g}, ${b})`;
        } else {
            // Low pulse: Forest Green (Deep shadow/Organic matter)
            const r = Math.floor(30 + pulse * 20);
            const g = Math.floor(100 + pulse * 40);
            const b = Math.floor(60 + pulse * 30);
            color = `rgb(${r}, ${g}, ${b})`;
        }

        ctx.beginPath();
        ctx.moveTo(p1.x, p1.y);
        ctx.lineTo(p2.x, p2.y);

        // --- UPDATED SHADOW COLOR ---
        // Using the same palette for the glow
        ctx.shadowBlur = depth * 2;
        ctx.shadowColor = color;

        ctx.strokeStyle = color;
        ctx.lineWidth = depth * 0.5 * pulse;
        ctx.stroke();

        ctx.shadowBlur = 0;

        // --- RECURSION ---
        const midX = (x1 + x2) / 2 + (Math.random() - 0.5) * depth * jitterScale;
        const midY = (y1 + y2) / 2 + (Math.random() - 0.5) * depth * jitterScale;
        const midZ = (z1 + z2) / 2 + (Math.random() - 0.5) * depth * jitterScale;

        if (Math.random() > 0.2) {
            drawBranch(x1, y1, z1, midX, midY, midZ, depth - 1, jitterScale);
            drawBranch(midX, midY, midZ, x2, y2, z2, depth - 1, jitterScale);
        }
    }

    let fps = 20;
    let fpsInterval = 1000 / fps;
    let lastDrawTime = performance.now();
    let isCancelled = false;

    function animate(currentTime) {
        if (isCancelled) return;
        let elapsed = currentTime - lastDrawTime;
        if (elapsed < fpsInterval) {
            requestAnimationFrame(animate);
            return;
        }
        lastDrawTime = currentTime - (elapsed % fpsInterval);

        // --- UPDATED BACKGROUND AND TRAIL ---
        // Change from dark void (5, 2, 15) to a warm, soft cream/beige
        // (rgb 245, 240, 220) and reduce opacity to keep the atmosphere light.
        ctx.fillStyle = 'rgba(245, 240, 220, 0.12)';
        ctx.fillRect(0, 0, width, height);
        angle += 0.015;

        const size = 150;
        const nodes = [
            [-size, -size, -size], [size, -size, -size],
            [size, size, -size], [-size, size, -size],
            [-size, -size, size], [size, -size, size],
            [size, size, size], [-size, size, size]
        ];

        // 1. Draw the Outer Box Edges
        for (let i = 0; i < 4; i++) {
            drawBranch(...nodes[i], ...nodes[(i + 1) % 4], 4);
            drawBranch(...nodes[i + 4], ...nodes[((i + 1) % 4) + 4], 4);
            drawBranch(...nodes[i], ...nodes[i + 4], 4);
        }

        // 2. Draw Bursts from Center (0,0,0) to each Corner
        nodes.forEach(node => {
            drawBranch(0, 0, 0, node[0], node[1], node[2], 6, 20);
        });

        // 3. Draw "Wild Static" shooting outward from corners into the void
        nodes.forEach(node => {
            const reach = 3.5;
            drawBranch(
                node[0], node[1], node[2],
                node[0] * reach, node[1] * reach, node[2] * reach,
                3, 25
            );
        });

        requestAnimationFrame(animate);
    }
    animate(0);
    return {
        dispose: () => {
            isCancelled = true;
            cancelAnimationFrame(requestId);
            window.removeEventListener('resize', resize);
        }
    };

}