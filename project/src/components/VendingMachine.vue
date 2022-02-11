<template>
  <p class="lbStyle">Выберите машину:</p>
  <div class="divcenter">
    <button
      class="buttonStyle"
      v-for="vendingMachine in vendingMachines"
      :key="vendingMachine.id"
      @click="$router.push(`/${vendingMachine.id}`)"
    >
      {{ vendingMachine.id }}
    </button>
  </div>
</template>

<script>
import axios from "axios";

//var baseURL = "https://192.168.0.134:7298/api/";
var baseURL = "https://192.168.0.206:7203/api/";

export default {
  data() {
    return {
      vendingMachines: [],
    };
  },

  methods: {
    async getVendingMachines() {
      this.vendingMachines = (
        await axios.get(baseURL + "VendingMachines")
      ).data;
    },
  },
  created() {
    this.getVendingMachines();
  },
};
</script>

<style>
.divcenter {
  position: relative;
  display: flex;
  align-items: center;
  justify-content: center;
}
.buttonStyle {
  width: 100px;
  height: 100px;
  margin: 10px;
  border: 2px solid black;
  border-radius: 40px;
  font-size: 20px;
  background: teal;
  color: white;
}

.lbStyle {
  position: relative;
  display: flex;
  align-items: center;
  justify-content: center;
  font-size: 50px;
}
</style>