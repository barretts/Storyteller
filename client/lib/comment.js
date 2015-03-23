var uuid = require('node-uuid');
var Arg = require('./arg');
var Cell = require('./cell');

class Comment{
	constructor(data){
		this.id = data.id || uuid.v4();

		var cell = new Cell('text', 'Textual comment');
		cell.editor = 'comment';

		this.arg = new Arg(cell, {cells: data}, this.id);
	}

	children(){
		return [];
	}

	clearResults(){}

	write(){
		return {text: this.arg.value, type: 'comment', id: this.id}
	}

	pack(){
		return null;
	}

	preview(loader){
		return this.editor(loader);
	}

	editor(loader){
		return loader.comment({step: this, arg: this.arg});
	}

	buildResults(loader){
		return this.editor(loader);
	}

	isHolder(){
		return false;
	}

	clearActiveState(){
		this.arg.active = false;
	}

	findByPath(path){
		if (path == 'text') return this.arg;

		return null;
	}
}



module.exports = Comment;