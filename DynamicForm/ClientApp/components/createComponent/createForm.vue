<template>
  <div>
    <div class="flex four">
      <div class="three-fourth">
        <input v-model="belongTo" placeholder="綁定跳題"/>
      </div>
    </div>


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

      <div>
        <button class='success' @click="newRecord">Add</button>
      </div>
    </div>

    <div>
      <createInput v-for="item in createRecord" :key="createRecord.id" @delRecord="delRecord"
                   :data="item"></createInput>
    </div>
    <button v-if="createRecord.length>0" class='warning' @click="finishQuestion">送出</button>

  </div>
</template>

<script>

  import createInput from '../../components/createComponent/createInput.vue';

  export default {
    props: {
      data: {
        type: Array,
      },
    },
    data() {
      return {
        createRecord: [],
        selectedType: '',
        createQuestionName: '',
        belongTo: '',
      };
    },
    components: {
      'createInput': createInput,
    },
    methods: {
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
          belong: this.belongTo,
          show: true,
        };
        this.data.push(object);
        this.createRecord.map((item, index) => {
          const object = {
            id: item.id,
            type: this.selectedType,
            displayName: item.name,
            value: '',
            belong: this.belongTo,
            show: true,
          };
          this.data.push(object);
        });
        console.log(this.data);
        this.resetForm();
      },
      resetForm: function () {
        this.createQuestionName = '';
        this.createRecord = [];
        this.selectedType = '';
        this.belongTo = '';
      },
    },
  };

</script>
