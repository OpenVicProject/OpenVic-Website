<script>
    import Header from "$lib/Header.svelte";
    import FAQ from "$lib/FAQ.svelte";
    import { json } from "@sveltejs/kit"
    import { onMount } from "svelte"

    //let faqs = [];
    //onMount(async () => {
    //    const res = await fetch("/api/faq");
    //    faqs = await res.json();
    //});
</script>

<Header/>
<!--{#each faqs as faq}
<FAQ q={faq.question} a={faq.answer}/>
{/each}!-->
<FAQ q="Are you guys Open-Source?" a="Yes! Our project is licensed under GPL3."/>
<FAQ q="What does this mean for Victoria 2 Mods?" a="We will create our own system of modding that is leagues more powerful and intuitive than PDX's current modding system for Victoria 2. However, we also plan to include full backwards compatibility for mods for Victoia 2. This means you will be able to drag your Mod files straight into OpenVic2, and they will work perfectly."/>
<FAQ q="Will OpenVic be free?" a="OpenVic will be free for anyone to play, forever."/>
<FAQ q="How much will you guys change?" a="We plan to emulate Victoria 2 as close as possible, even the quirks, although after 1.0, we will make improvements to the game that can be disabled."/>
<FAQ q="Doesn't this violate copyright?" a="No, everything included in OpenVic is entirely created by the team or open-source."/>
<FAQ q="Won't Paradox sue you anyway?" a="No, we have communicated with Paradox and recieved their sanction to go ahead without the threat of undeserved legal action."/>
<FAQ q="What can I do to help?" a="See 'Contributing'"/>
<FAQ q="How long will it take to finish?" a="A while. Quality development takes time, and for something the scope of this project that means likely in the neighbourhood of 2 years."/>