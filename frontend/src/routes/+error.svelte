<script>
    import { page } from '$app/stores';
</script>

<h1>{$page.status}: {$page.error.message}</h1>
{#if $page.status == 404}
    <h1>oops, looks like that doesn't exist!</h1>
    <h3>if you're sure you've got the link right, try reloading or going back</h3>
{:else}
    <h1>oops, there's some sort of issue</h1>
    <h3>try reloading, and if that doesn't work contact us!</h3>
{/if}
<a href="/">go home</a>

<style>
    :root {
        text-align: center;
        color: white;
    }
    a {
        color: white;
    }
    a:hover {
        text-decoration: underline;
        color: #CD7F32;
    }
</style>