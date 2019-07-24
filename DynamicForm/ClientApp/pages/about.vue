<template>
  <div>

    <div style="display:flex">
      <div>
        <div>
          <textarea v-model="textBox" cols="70" rows="10"></textarea>
        </div>
        <button @click="genReport">Commit</button>

      </div>


      <div class="rightSide">
        <dynamic v-for="(item,index) in data" :key="data.displayName"
                 :data="item"
                 @answerChange="updateForm"
        ></dynamic>
      </div>
    </div>


  </div>
</template>


<script>
  import dynamic from '../components/test/dynamic.vue';

  export default {
    data() {
      return {
        textBox: '[{"id":"1","type":"question","displayName":"你抽菸嗎","value":""},{"id":"2","type":"checkBox","displayName":"抽都抽","value":""},{"id":"3","type":"checkBox","displayName":"哥不抽","value":""},\n' +
                 '{"id":"4","type":"question","displayName":"一天抽幾次","value":"","belong":"2"},\n' +
                 '{"id":"5","type":"checkBox","displayName":"10次","value":"","belong":"2"},{"id":"6","type":"checkBox","displayName":"100次","value":"","belong":"2"}]',
        data: [],
      };
    },
    components: {
      'dynamic': dynamic,
    },
    created() {
      console.log(JSON.stringify(this.data));
    },
    methods: {
      genReport: function () {
        // parse to JSON
        let result = this.parseToObject(this.textBox);
        // if no err then set belong to
        if (result) {
          this.processBelongTo(result);
        }
      },
      parseToObject: function (jsonString) {
        try {
          return JSON.parse(jsonString);
        } catch (err) {
          return false;
        }
      },
      processBelongTo: function (data) {
        data.map(function (element, index, array) {
          if (element.belong) {
            const sta = array.find((a) => a.id === element.belong);
            element.show = (sta.value === true);
          } else {
            element.show = true;
          }
        });
        this.data = data;
      },
      updateForm: function (id, value) {
        this.data.map(function (element) {
          if (element.belong === id) {
            element.show = value;
          }
        });
      },
    },
  };
</script>

<style scoped>
  .rightSide {
    margin: 10px 70px;

  }
</style>
