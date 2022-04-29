import {Component} from 'react';
import {Modal,Button, Row, Col, Form} from 'react-bootstrap';

export class AddBookModal extends Component{
    constructor(props){
        super(props);
        this.state={genres:[], authors:[]}
        this.handleSubmit = this.handleSubmit.bind(this);
    }

    getGenresList(){
        fetch("http://localhost:5147/api/BookShelves/all-genres")
        .then(response => response.json())
        .then(data => {
            this.setState({genres: data})
        });
    }

    getAuthorsList(){
        fetch("http://localhost:5147/api/BookShelves/all-authors")
        .then(response => response.json())
        .then(data => {
            this.setState({authors: data})
        });
    }

    componentDidMount(){
        this.getGenresList();
        this.getAuthorsList();
    }

    componentDidUpdate(){     
    }

    handleSubmit(event){       
        event.preventDefault();
        
        const requestOptions = {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({
                name: event.target.BookName.value,
                year: event.target.BookYear.value,
                authorId: event.target.BookAuthor.value,
                genresId: [
                    event.target.BookGenre.value
                ]
            })
        };
        fetch('http://localhost:5147/api/BookShelves/add-book', requestOptions)
            .then(response => response.json())
            .then(data => console.log(data));
    }

    render(){
        const {genres, authors} = this.state;

        return <div className="container">

        <Modal
        {...this.props}
        size="lg"
        aria-labelledby="contained-modal-title-vcenter"
        centered
        >
            <Modal.Header clooseButton>
                <Modal.Title id="contained-modal-title-vcenter">
                    Add Book
                </Modal.Title>
            </Modal.Header>
            <Modal.Body>        
                <Row>
                    <Col>
                        <Form onSubmit={this.handleSubmit}>
                            <Form.Group controlId="BookName">
                                <Form.Label>Book name :</Form.Label>
                                <Form.Control className='formControl' type="text" name="BookName" required 
                                placeholder="BookName"/>
                            </Form.Group>

                            <Form.Group controlId="BookYear">
                                <Form.Label>Book year :</Form.Label>
                                <Form.Control type="text"  name="BookYear" formatSubmit="yyyy" required 
                                placeholder="BookYear"/>
                            </Form.Group>

                            <Form.Group controlId="BookGenre">
                                <Form.Label>Book genre :</Form.Label>
                                <Form.Select  name="BookGenre" required 
                                placeholder="BookGenre">
                                    {
                                        genres.map(genre => 
                                            <option key={genre.id} value={genre.id}>
                                                {genre.name}
                                            </option>)
                                    }
                                </Form.Select>
                            </Form.Group>

                            <Form.Group controlId="BookAuthor">
                                <Form.Label>Book author :</Form.Label>
                                <Form.Select  name="BookAuthor" required 
                                placeholder="BookAuthor">
                                    {
                                        authors.map(author => 
                                            <option key={author.id} value={author.id}>
                                                {author.name}
                                            </option>)
                                    }
                                </Form.Select>
                            </Form.Group>
        
                            <Form.Group>
                                <Button className='mt-2' variant="primary" type="submit">
                                    Add Book
                                </Button>
                            </Form.Group>
                        </Form>
                    </Col>
                </Row>
            </Modal.Body>
            
            <Modal.Footer>
                <Button variant="danger" onClick={this.props.onHide}>Close</Button>
            </Modal.Footer>
        
        </Modal>
        
        </div> 
    }
}