<script>
    export let mdfile;
    
    import { onMount } from "svelte";
    import { marked } from 'marked';
    import { baseUrl } from 'marked-base-url';
    import DOMPurify from 'dompurify';

    const BASE_URL = "https://raw.githubusercontent.com/OpenVicProject/OpenVic/master/docs/";
    var domstring = "<h2>loading...</h2>"
    
    onMount(() => {
        marked.use(baseUrl(BASE_URL));
        fetch(BASE_URL + mdfile)
            .then((res) => res.text())
            .then((mdt) => {
                domstring = marked.parse(mdt);
                const regex = /https:\/\/raw\.githubusercontent\.com\/OpenVicProject\/OpenVic\/master\/docs\/.*\.md/i;
                const subregex = /\.md/i;
                var i = 1;
                while(i>0) {
                    i = domstring.search(regex);
                    var resource = domstring.substring(i);
                    const x = resource.search(subregex) + 3;
                    resource = "/docs/" + resource.substring(69, x);
                    domstring = domstring.replace(regex, resource);
                }
                document.getElementById('md-doc-content').innerHTML = DOMPurify.sanitize(domstring);
            });
    });
</script>

<div id="md-doc-content">{@html domstring}</div>