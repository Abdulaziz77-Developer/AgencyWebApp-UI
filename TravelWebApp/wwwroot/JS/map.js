window.leafletBridge = {
    createRoute: function (mapId, lat, lng, title) {
        // удаляем предыдущую карту, если есть
        if (window[mapId]) {
            window[mapId].remove();
        }

        // создаем карту
        var map = L.map(mapId).setView([lat, lng], 13);

        // сохраняем карту в глобальном объекте
        window[mapId] = map;

        // слой OpenStreetMap
        L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
            attribution: '&copy; OpenStreetMap contributors'
        }).addTo(map);

        // маркер тура
        L.marker([lat, lng]).addTo(map)
            .bindPopup(title)
            .openPopup();
    }
};
