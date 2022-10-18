<template>
  <nav class="navbar navbar-expand-lg nav-color px-3 justify-content-between">
      <router-link class="navbar-brand" :to="{ name: 'Home' }">
          <img alt="logo" title="Home" name="Home" src="https://upload.wikimedia.org/wikipedia/commons/thumb/d/dc/Los_K_Morales_Logo_2013.svg/1200px-Los_K_Morales_Logo_2013.svg.png" height="45" />
      </router-link>=
      <div class="col-5 text-center">
      </div>
      <!-- LOGIN COMPONENT HERE -->
      <Login />
  </nav>
</template>

<script>
export default {
  setup() {
    return {};
  },
};
</script>

<style scoped>
  .nav-color{
    background-color: #55EFC4;;
  }
.text-test{
  color: black;
}
</style>
