export const UrlHelper = {
    getUrl: (url: string | null) => {
        if (!Boolean(url) || url == null) {
            return;
        }

        if (!url.includes('https') && !url.includes('http')) {
            return `https://${url}`;
        }
        return url;
    }
}