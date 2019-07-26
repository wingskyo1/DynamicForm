<template>
  <div>

    <div class="flex two">
      <!--左半-->
      <div>
        <!--填寫 Json區塊-->
        <div>
          <textarea v-model="textBox" cols="70" rows="10"></textarea>
        </div>
        <button @click="genReport(textBox)">Commit</button>
        <button @click="genJson()">Commit</button>

        <!--自訂選擇區塊-->
        <div class="flex four">

          <div class="half">
            <input v-model="createQuestionName" placeholder="題目名稱"/>
          </div>

          <div>
            <select v-model="selectedType">
              <option>Type</option>
              <option value="radio">單選題</option>
              <option value="checkBox">多選題</option>
              <option value="textBox">TextBox</option>
            </select>
          </div>
          <div class="two">
            <input placeholder="綁定跳題"/>
            <button class='success' @click="newRecord">Add</button>
          </div>
        </div>

        <div>
          <createInput v-for="item in createRecord" :key="createRecord.id" @delRecord="delRecord"
                       :data="item"></createInput>
        </div>
        <button v-if="createRecord.length>0" class='warning' @click="finishQuestion">送出</button>

      </div>

      <!--右半 顯示問卷-->
      <div class="">
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
  import createInput from '../components/createComponent/createInput.vue';

  export default {
    data() {
      return {
        textBox: '[{"id":"1","type":"question","displayName":"你抽菸嗎","value":""},{"id":"2","type":"checkBox","displayName":"抽都抽","value":""},{"id":"3","type":"checkBox","displayName":"哥不抽","value":""},\n' +
                 '{"id":"4","type":"question","displayName":"一天抽幾次","value":"","belong":"2"},\n' +
                 '{"id":"5","type":"checkBox","displayName":"10次","value":"","belong":"2"},{"id":"6","type":"checkBox","displayName":"100次","value":"","belong":"2"}]',
        data: [],
        createRecord: [],
        selectedType:'',
        createQuestionName:'',
      };
    },
    components: {
      'dynamic': dynamic,
      'createInput': createInput,
    },
    created() {
      console.log(JSON.stringify(this.data));
    },
    methods: {
      genJson: function (){
        const tmp = JSON.parse(JSON.stringify(this.data));
        tmp.map(item=> delete item.show);
        this.textBox = JSON.stringify(tmp);
      },

      genReport: function (JsonString) {
        // parse to JSON Object
        let result = this.parseToObject(JsonString);
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
      newRecord: function () {
        const object = {id: this.IDGenerator(), name: ''};
        this.createRecord.push(object);
      },
      IDGenerator: function () {
        // always start with a letter (for DOM friendlyness)
        let idStr = String.fromCharCode(Math.floor((Math.random() * 25) + 65));
        do {
          // between numbers and characters (48 is 0 and 90 is Z (42-48 = 90)
          const ascicode = Math.floor((Math.random() * 42) + 48);
          if (ascicode < 58 || ascicode > 64) {
            // exclude all chars between : (58) and @ (64)
            idStr += String.fromCharCode(ascicode);
          }
        } while (idStr.length < 32);
        return (idStr);
      },
      delRecord: function (id) {
        const index = this.createRecord.findIndex(obj => obj.id === id);
        this.createRecord.splice(index, 1);
      },
      finishQuestion: function () {
        const object = {
          id: this.IDGenerator(),
          type: 'question',
          displayName: this.createQuestionName,
          value: '',
          belong: '',
          show:true,
        };

        this.data.push(object);

        this.createRecord.map((item, index) => {
          const object = {
            id: item.id,
            type: this.selectedType,
            displayName: item.name,
            value: '',
            belong: '',
            show:true,
          };
          this.data.push(object);
        });

        console.log(this.data)
      },
    },
  };
</script>

<style scoped>
  .rightSide {
    margin: 10px 70px;

  }
</style>
