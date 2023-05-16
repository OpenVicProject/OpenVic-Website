<script>
    function goHome() {
      window.location.href = "/"
    }
</script>
  
<div id="header-background">
  <div id="header">
    <!-- svelte-ignore a11y-click-events-have-key-events -->
    <img src="/ov.png" on:click={goHome} id="ov2img" alt="OpenVic Logo" width="8%" height="8%">
    <a href="/faqs">faqs</a>
    <a href="/news">news</a>
    <a href="/about">about</a>
    <a href="/contribute">contribute</a>
  </div>
</div>

<style>
  #header-background {
    text-align: center;
    background-color: #AB8E76;
    margin: 1%;
    padding: 0.5%;
    border-radius: 2.5vw;
  }

  #header {
    display: flex;
    flex-direction: row;
    justify-content: space-evenly;
    align-items: center;
    text-align: left;
    background-color: #FFFDD0;
    border-radius: 2.1vw;
  }

  #ov2img {
    filter: brightness(0) saturate(100%) invert(62%) sepia(44%) saturate(309%) hue-rotate(355deg) brightness(85%) contrast(85%);
  }

  #ov2img:hover {
    filter: brightness(0) saturate(100%) invert(78%) sepia(63%) saturate(3227%) hue-rotate(335deg) brightness(84%) contrast(90%);
    cursor: pointer;
  }

  a {
    font-size: 5vw;
  }

  a:hover {
    color: #CD7F32;
    text-decoration: none;
  }
</style>
