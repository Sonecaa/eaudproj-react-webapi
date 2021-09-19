import React, { Component } from 'react';
import { Button, Input, Form} from 'antd';
import { EditOutlined } from '@ant-design/icons';
import { DeleteOutlined } from '@ant-design/icons';


export class FetchData extends Component {
  static displayName = FetchData.name;

  constructor(props) {
    super(props);
    this.state = { data: [], loading: true, fields: [], entity: ""};
  }

  componentDidMount() {
    this.populateData();
  }

  static renderDataTable(data, fields) {
    return (
      <table className='table table-striped' aria-labelledby="tabelLabel">
        <thead>
          {this.getTableHeaders(fields)}
        </thead>
        <tbody>

          {Array.isArray(data) && data.map(each =>
            <tr key={each.id}>

              {Object.keys(each).map((propety) => <td> {each[propety]} </td>)}
              <td><Button type="primary" icon={<EditOutlined />} /></td>
              <td><Button type="primary" danger icon={<DeleteOutlined />} /></td>
            </tr>
          )}
        </tbody>
      </table>
    );
  }

  static getTableHeaders(fields) {

    return (
      <tr>

        {fields.map(x =>
          <th>{x}</th>
        )}
        <th></th>
        <th></th>

      </tr>
    )

  }

  static renderInsertRow(fields, that) {

    const onFinish = async (values) => {
      const requestOptions = {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(values)
      };

      const response = await fetch('api/movie', requestOptions);
      const data = await response.json();
      that.setState({ data: data, loading: false });
    };

    return (
      <Form
      name="create_form"
      onFinish={onFinish}
      >

        {fields.map(each =>
          <Form.Item label={each} name={each}>
            <Input />
          </Form.Item>
        )}

        <Form.Item>
          <Button type="primary" htmlType="submit">Submit</Button>
        </Form.Item>

      </Form>
    );
  }

  render() {
    this.state.entity = this.props.location.pathname;
    this.state.fields = this.getEntityFields();
    let contents = this.state.loading
      ? <p><em>Loading...</em></p>
      : FetchData.renderDataTable(this.state.data, this.state.fields);

    return (
      <div> 
        <h1 id="tabelLabel" >{this.state.entity}'s</h1>
        {FetchData.renderInsertRow(this.state.fields, this)}
        <p>Qtd: {Object.keys(this.state.data).length}</p>
        {contents}
      </div>
    );
  }

   getEntityFields() {

    const entity = this.state.entity;
    var entityFields = [];

    if (entity == "/movie") {

      entityFields = ['id', 'titulo', 'classificacaoIndicativa', 'lancamento'];

    } else if(entity == "/location") {

      entityFields = ['id', 'id_cliente', 'id_filme', 'dataLocacao', 'dataDevolucao'];

    } else if(entity == "/costumer") {

      entityFields = ['id', 'nome', 'cpf', 'dataNascimento'];

    }

    return entityFields;

  }

  async populateData() {
    const response = await fetch('api' + this.state.entity);
    const data = await response.json();
    this.setState({ data: data, loading: false });
  }

}
