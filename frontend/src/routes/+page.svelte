<script>
    import Header from "$lib/Header.svelte";
</script>

<Header/>
<a href="https://openvicstorage.blob.core.windows.net/dev-diaries/Dev_diary_4.pdf" target="_blank" rel="noopener noreferrer">
    <img id="map" src="/terrain-map-4-5-23.png" alt="OpenVic Terrain Map Mode, Centered on Europe"><br>
    Dev Diary IV: the Map
</a>

<style>
    img {
        aspect-ratio: 16/9;
    }
    a {
        font-size: 3.5vw;
    }
    #map {
        width: 42vw;
    }
</style>
