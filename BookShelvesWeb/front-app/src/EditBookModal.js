import { Component } from 'react';
import { Modal, Button, Row, Col, Form } from 'react-bootstrap';

export class EditBookModal extends Component {
    constructor(props) {
        super(props);
        this.state = { genres: [], authors: [] }
        this.handleSubmit = this.handleSubmit.bind(this);
    }

    getGenresList() {
        fetch("http://localhost:5147/api/BookShelves/all-genres")
            .then(response => response.json())
            .then(data => {
                this.setState({ genres: data })
            });
    }

    getAuthorsList() {
        fetch("http://localhost:5147/api/BookShelves/all-authors")
            .then(response => response.json())
            .then(data => {
                this.setState({ authors: data })
            });
    }

    componentDidMount(){
        this.getGenresList();
        this.getAuthorsList();
    }

    componentDidUpdate(){     
    }

    handleSubmit(event) {
        event.preventDefault();

        const requestOptions = {
            method: 'PUT',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({
                id: event.target.BookId.value,
                name: event.target.BookName.value,
                year: event.target.BookYear.value,
                authorId: event.target.BookAuthor.value,
                genresId: [
                    event.target.BookGenre.value
                ]
            })
        };

        fetch("http://localhost:5147/api/BookShelves/update-book", requestOptions)
            .then(res => res.json())
            .then((result) => {
                alert(result);
            },
            (error) => {
                alert('Failed');
            })
    }

    render() {
        const { genres, authors } = this.state;
        return (
            <div className="container">

                <Modal
                    {...this.props}
                    size="lg"
                    aria-labelledby="contained-modal-title-vcenter"
                    centered
                >
                    <Modal.Header clooseButton>
                        <Modal.Title id="contained-modal-title-vcenter">
                            Edit Book
                        </Modal.Title>
                    </Modal.Header>
                    <Modal.Body>

                        <Row>
                            <Col >
                                <Form onSubmit={this.handleSubmit}>

                                    <Form.Group controlId="BookId">
                                        <Form.Label>Book Id :</Form.Label>
                                        <Form.Control type="text" name="BookId" required
                                            defaultValue={this.props.bookId} disabled={true}
                                            placeholder="BookId" />
                                    </Form.Group>

                                    <Form.Group controlId="BookName">
                                        <Form.Label>Book name :</Form.Label>
                                        <Form.Control type="text" name="BookName" required
                                            defaultValue={this.props.bookName}
                                            placeholder="BookName" />
                                    </Form.Group>

                                    <Form.Group controlId="BookYear">
                                        <Form.Label>Book year :</Form.Label>
                                        <Form.Control type="text" name="BookYear" formatSubmit="yyyy" required
                                            defaultValue={this.props.bookYear}
                                            placeholder="BookYear" />
                                    </Form.Group>

                                    <Form.Group controlId="BookGenre">
                                        <Form.Label>Book genre :</Form.Label>
                                        <Form.Select name="BookGenre" required

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
                                        <Form.Select name="BookAuthor" required
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
                                        <Button variant="primary" type="submit">
                                            Update Book
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
        )
    }

}