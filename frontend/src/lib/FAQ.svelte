<script>
    export let q;
    export let a;

    let showing = false;

    function onClick() {
        showing = !showing;
    }
</script>

<div>
{#if showing}
    <!-- svelte-ignore a11y-click-events-have-key-events -->
    <h2 on:click={onClick}>{q}</h2>
    <p>{a}</p>
    {:else}
    <!-- svelte-ignore a11y-click-events-have-key-events -->
    <h2 on:click={onClick}>{q}</h2>
{/if}
</div>

<style>
    div {
        margin: 0vw 1vw;
    }
    p {
        background-color: #00000090;
        box-shadow: 0vw 0vw 0.5vw 0.5vw #00000090;
        border-radius: 2vw;
        font-size: 1.5vw;
        padding: 1vw;
    }
    h2:hover {
        cursor: pointer;
        text-decoration: underline;
        color: #CD7F32;
    }
</style>