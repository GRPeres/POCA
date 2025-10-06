self.addEventListener('install', event => {
    console.log('Service Worker: Installing...');
    self.skipWaiting();
});

self.addEventListener('activate', event => {
    console.log('Service Worker: Activated');
    event.waitUntil(clients.claim());
});

self.addEventListener('fetch', event => {
    if (event.request.method !== 'GET') return;

    event.respondWith(
        caches.open('blazor-pwa-cache-v1').then(async cache => {
            const cachedResponse = await cache.match(event.request);
            if (cachedResponse) {
                return cachedResponse;
            }

            try {
                const networkResponse = await fetch(event.request);
                if (networkResponse.ok) {
                    cache.put(event.request, networkResponse.clone());
                }
                return networkResponse;
            } catch {
                return cachedResponse || Response.error();
            }
        })
    );
});
