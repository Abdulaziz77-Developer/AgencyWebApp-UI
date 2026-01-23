window.leafletBridge = {
    maps: {},
    showLocation: function (containerId, lat, lon, title) {
        console.log("Отрисовка карты в:", containerId);

        // 1. Если карта на этом элементе уже была — удаляем её
        if (this.maps[containerId]) {
            this.maps[containerId].remove();
        }

        const el = document.getElementById(containerId);
        if (!el) {
            console.error("Элемент не найден:", containerId);
            return;
        }

        try {
            // 2. Инициализация
            var map = L.map(containerId).setView([lat, lon], 13);
            this.maps[containerId] = map;

            L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
                attribution: '© OpenStreetMap contributors'
            }).addTo(map);

            L.marker([lat, lon]).addTo(map)
                .bindPopup(title)
                .openPopup();

            // 3. Исправление отображения (серый экран)
            setTimeout(function () {
                map.invalidateSize();
            }, 200);

        } catch (e) {
            console.error("Ошибка при создании карты:", e);
        }
    }
};