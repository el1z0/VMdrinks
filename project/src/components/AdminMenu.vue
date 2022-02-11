<template>
  <div class="divcenter">
    <p>Административная панель</p>
  </div>
  <div class="divcenter">
    <button @click="$router.push(`/admin/${this.$route.params.scode}/Coins`)">Монеты</button>
    <button>Напитки</button>
    <button>Отчёт</button>
  </div>
</template>

<script>
import axios from "axios";

//var baseURL = "https://192.168.0.134:7298/api/";
var baseURL = "https:/192.168.0.106:7092/api/";

export default {
  name: "app",
  data() {
    return {
      coin: [],
      vmcoin: [],
    };
  },
  methods: {
    async getData() {},
  },
  async mounted() {
    this.getData();
  },
};
</script>

<style scoped>
.divcenter {
  position: relative;
  display: fixed;
  align-items: center;
  justify-content: center;
}
</style>