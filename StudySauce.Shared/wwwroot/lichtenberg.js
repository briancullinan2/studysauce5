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

        const pulse = (Math.cos(angle + (depth * 0.2)) + 1) / 2;
        const r = Math.floor(5 + pulse * 50);
        const g = Math.floor(39 + pulse * 20);
        const b = Math.floor(103 + pulse * 152);

        ctx.beginPath();
        ctx.moveTo(p1.x, p1.y);
        ctx.lineTo(p2.x, p2.y);
        ctx.strokeStyle = `rgb(${r}, ${g}, ${b})`;
        ctx.lineWidth = depth * 0.4 * pulse;
        ctx.stroke();

        const midX = (x1 + x2) / 2 + (Math.random() - 0.5) * depth * jitterScale;
        const midY = (y1 + y2) / 2 + (Math.random() - 0.5) * depth * jitterScale;
        const midZ = (z1 + z2) / 2 + (Math.random() - 0.5) * depth * jitterScale;

        if (Math.random() > 0.2) {
            drawBranch(x1, y1, z1, midX, midY, midZ, depth - 1, jitterScale);
            drawBranch(midX, midY, midZ, x2, y2, z2, depth - 1, jitterScale);
        }
    }

    function animate() {
        // Darker trail for high-contrast static
        ctx.fillStyle = 'rgba(5, 2, 15, 0.15)';
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
        // Increased jitterScale (20) makes center bursts look more erratic
        nodes.forEach(node => {
            drawBranch(0, 0, 0, node[0], node[1], node[2], 6, 20);
        });

        // 3. Draw "Wild Static" shooting outward from corners into the void
        nodes.forEach(node => {
            // Shoots 1.5x further out than the corner
            drawBranch(node[0], node[1], node[2], node[0] * 1.5, node[1] * 1.5, node[2] * 1.5, 3, 25);
        });

        requestAnimationFrame(animate);
    }
    animate();
}