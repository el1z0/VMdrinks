<template>
  <div class="divItemsDrink">
    <div class="split left">
      <button
        class="btnDrink"
        @click="sellingDrink(drink)"
        v-for="drink in drink"
        :key="drink.id"
        :disabled="lockedDrinks(drink) <= 0"
      >
        <img :src="'data:text/plain;base64,' + drink.image" class="imgDrink" />
        <p class="drinkname">{{ drink.name }}</p>
        <p class="drinkname">{{ drink.cost }}р</p>
      </button>
    </div>

    <div class="split right">
      <div class="divM">
        <label class="lbCount">{{ money }} руб.</label>
        <button
          @click="getMoney(coin.denomination)"
          v-for="coin in coin"
          :key="coin.id"
          class="btnCoins"
          :disabled="lockedCoin(coin) == 0"
        >
          {{ coin.denomination }}
        </button>
        <div class="instruct">
          <p class="p">1. Внесите деньги</p>
          <p class="p">2. Выберите напиток</p>
          <p class="p">3. Получите сдачу</p>
        </div>
      </div>
      <button class="btnSdacha" @click="sdacha">Сдача</button>
    </div>
  </div>
</template>




<script>
import axios from "axios";

//var baseURL = "https://192.168.0.134:7298/api/";
var baseURL = "https://192.168.0.206:7203/api/";

export default {
  name: "app",
  data() {
    return {
      drink: [],
      vmdrink: [],
      coin: [],
      vmcoin: [],
      money: 0,
    };
  },

  methods: {
    
    async getMoney(coin) {
      this.money += coin;
      const addCoin = this.vmcoin.find((x) => x.coinsId == coin.id);
      addCoin.count += 1;
      await axios.put(baseURL + `VendingMachineCoins/${addCoin.id}`, addCoin);
    },
    async sellingDrink(drink) {
      try {
        if (this.money < drink.cost) {
          alert("Недостаточно средств!");
          return this.money;
        }
        this.money -= drink.cost;

        const minusC = this.vmdrink.find((x) => x.drinksId == drink.id);
        minusC.count -= 1;
        await axios.put(baseURL + `VendingMachineDrinks/${minusC.id}`, minusC);

        alert("Сдача равна " + this.money + " руб.");
        return this.money;
      } catch (ex) {
        console.log(ex);
      }

      // if (this.money >= drink.cost) {
      //   this.money -= drink.cost;
      //   alert("Выбрали " + drink.name + " за " + drink.cost + " руб.");
      // }
    },
async sdacha() {
      const minusSdacha = this.vmcoin.filter((x) => x.count > 0);
      const coins = this.coin.filter((x) =>
        minusSdacha.some((y) => y.coinsId == x.id)
      );
      coins.reverse();
      for (let i = 0; i < coins.length; i++) {
        const parseCoin = parseInt(coins[i].denomination);

        if (this.money <= 0 && parseCoin > this.money) {
          continue;
        }
        const find_coin = minusSdacha.find((x) => x.coinsId == coins[i].id);
        const res_min = Math.floor(
          Math.min(find_coin.count, this.money / parseCoin)
        );
        console.log(res_min);
        if (find_coin.count >= res_min && this.money % parseCoin > 0) {
          res_min -= 1;
        }

        this.money -= res_min * parseCoin;
        const resultCoins = this.vmcoin.find((x) => x.coinsId == coins[i].id);
        resultCoins.count -= res_min;
        await axios.put(
          baseURL + `VendingMachineCoins/${resultCoins.id}`,
          resultCoins
        );
      }
      alert("Сдача " + this.money + " руб.");
      this.money = 0;
    },
    async getData() {
      try {
        const vmdrinks = await axios.get(baseURL + "VendingMachineDrinks");
        this.vmdrink = vmdrinks.data.filter(
          (x) => x.vendingMashineId == this.$route.params.vendingname
        );

        const drinks = await axios.get(baseURL + "Drinks");
        this.drink = drinks.data.filter((x) =>
          this.vmdrink.some((y) => y.drinksId == x.id)
        );
        this.getCoins();
      } catch (ex) {
        alert(ex);
      }
    },
    async getCoins() {
      this.vmcoin = (
        await axios.get(baseURL + "VendingMachineCoins")
      ).data.filter(
        (x) => x.vendingMashineId == this.$route.params.vendingname
      );

      this.coin = (await axios.get(baseURL + "Coins")).data.filter((x) =>
        this.vmcoin.some((y) => y.coinsId == x.id)
      );
    },

    lockedCoin(coin) {
      return this.vmcoin.some((x) => x.coinsId == coin.id && x.isActive == 1);
    },
    lockedDrinks(drink) {
      return this.vmdrink.some((x) => x.drinksId == drink.id && x.count > 0);
    },
  },
  mounted() {
    this.getData();
  },
};
</script>


<style scoped>
.divItemsDrink {
  border: 3px solid;
  border-radius: 10px;
  height: 500px;
}
/*позиция*/
.split {
  position: fixed;
  padding-top: 20px;
}
.left {
  left: 0;
  height: 100%;
  width: 70%;
  background-color: transparent;
}
.right {
  border: 3px solid;
  border-radius: 10px;
  right: 0;
  height: 95%;
  width: 30%;
  margin-top: -3px;
  margin-right: 5px;
  background-color: white;
}

/*левая часть*/
.btnDrink {
  position: relative;
  left: 20%;
  height: 200px;
  width: 150px;
  margin: 10px;
  background: transparent;
  border: 2px dotted;
  border-radius: 20px;
}
.btnDrink:hover {
  /*наведение*/
  position: relative;
  left: 20%;
  height: 200px;
  width: 150px;
  margin: 10px;
  background: transparent;
  border: 2.5px dotted green;
  border-radius: 20px;
}
.btnDrink:focus {
  position: relative;
  left: 20%;
  height: 200px;
  width: 150px;
  margin: 10px;
  background: rgb(224, 224, 224);
  border: 2.5px dotted red;
  border-radius: 20px;
}

.imgDrink {
  height: 150px;
  width: 100%;
  vertical-align: center;
}

.drinkname {
  position: relative;
  display: block;
  padding: 0;
  margin: 0;
  font-weight: bold;
}
/*правая часть*/
.divM {
  position: relative;
  display: block;
  align-items: center;
  justify-content: center;
  padding-top: 10%;
  padding-left: 20%;
  padding-right: 20%;
}
.lbCount {
  position: relative;
  display: block;
  width: 100%;
  height: 60px;
  border: 2px solid black;
  border-radius: 10px;
  text-align: center;
  font-size: 20px;
  padding-top: 10%;
  margin-bottom: 10%;
}
.btnCoins {
  position: relative;
  display: inline;
  align-items: center;
  justify-content: center;
  left: 15%;
  right: 15%;
  margin: 10px;
  height: 100px;
  width: 100px;
  font-size: 18px;
  background: rgb(196, 196, 196);
  color: white;
}
.instruct {
  position: relative;
  display: block;
  align-items: center;
  justify-content: center;
  border: 2px solid black;
  border-radius: 5px;
}
.p {
  font-size: 20px;
  text-align: center;
}
.btnSdacha {
  position: relative;
  top: 5%;
  margin: 5%;
  width: 90%;
  height: 50px;
  background: teal;
  color: white;
  font-size: 18px;
}
.btnAdmin {
  position: relative;
  margin: 20px;
}
</style>