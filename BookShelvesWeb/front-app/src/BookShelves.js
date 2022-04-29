import { Component } from "react";
import { Button, ButtonToolbar, Table } from 'react-bootstrap';

import { AddBookModal } from './AddBookModal';
import { EditBookModal } from "./EditBookModal";

export class BookShelves extends Component {

    constructor(props) {
        super(props);
        this.state = { books: [], addModalShow: false, editModalShow: false }
    }

    refreshList() {        
        fetch(process.env.BOOK_SHELVES_API)
            .then(response => response.json())
            .then(data => { this.setState({ books: data } )
            });
    }

    componentDidMount() {
         this.refreshList();
    }

    componentDidUpdate() {      
    }

    deleteBook(bookId) {
        if (window.confirm('Are you sure?')) {
            fetch(process.env.BOOK_SHELVES_API + "delete-book?id=" + bookId, {
                method: 'DELETE',
                header: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                }
            })
        }
    }

    render() {
        const { books } = this.state;
        let addModalClose = () => this.setState({ addModalShow: false });
        let editModalClose = () => this.setState({ editModalShow: false });

        return <div>
            <Table className="mt-4" striped bordered hover size="sm">
                <thead>
                    <tr>
                        <th>Id</th>
                        <th>Name</th>
                        <th>Year</th>
                        <th>Genre</th>
                        <th>Autor</th>
                        <th>Actions</th>
                    </tr>
                </thead>
                <tbody>
                    {
                        books.map(book => {
                            return <tr key={book.Id}>
                                <td>{book.id}</td>
                                <td>{book.name}</td>
                                <td>{book.year}</td>
                                <td>{book.bookGenres.map(bookGenre => bookGenre.genre.name + "\n")}</td>
                                <td>{book.author.name}</td>
                                <td>

                                    <ButtonToolbar>

                                        <Button className="mr-2" variant="info"
                                            onClick={() => this.setState({
                                                editModalShow: true
                                            })}>
                                            Edit
                                        </Button>

                                        <Button className="mr-2" variant="danger"
                                            onClick={() => this.deleteBook(book.id)}>
                                            Delete
                                        </Button>

                                        <EditBookModal show={this.state.editModalShow}
                                            onHide={editModalClose}
                                            bookId={book.id}
                                            bookName={book.name}
                                            bookYear={book.year}
                                        />

                                    </ButtonToolbar>

                                </td>
                            </tr>
                        })
                    }
                </tbody>
            </Table>

            <ButtonToolbar>
                <Button variant='primary'
                    onClick={() => this.setState({ addModalShow: true })}>
                    Add Department</Button>

                <AddBookModal show={this.state.addModalShow}
                    onHide={addModalClose}>

                </AddBookModal>

            </ButtonToolbar>

        </div>
    }
}