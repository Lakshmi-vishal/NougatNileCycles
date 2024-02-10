import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { makeStyles } from '@material-ui/core/styles';
import {
    IconButton,
    Badge,
    TextField,
    Paper,
    Typography,
    Table,
    TableBody,
    TableCell,
    TableContainer,
    TableHead,
    TableRow,
} from '@material-ui/core';
import AddIcon from '@material-ui/icons/Add';
import RemoveIcon from '@material-ui/icons/Remove';
import ShoppingCartIcon from '@material-ui/icons/ShoppingCart';
import ArrowBackIcon from '@material-ui/icons/ArrowBack';
import { useNavigate } from 'react-router-dom';

const useStyles = makeStyles((theme) => ({
    table: {
        minWidth: 650,
    },
    tableContainer: {
        borderRadius: 15,
        margin: theme.spacing(2),
        maxWidth: '95%',
    },
    tableHeaderCell: {
        fontWeight: 'bold',
        backgroundColor: theme.palette.primary.dark,
        color: theme.palette.getContrastText(theme.palette.primary.dark),
    },
    title: {
        padding: theme.spacing(2),
        color: theme.palette.primary.dark,
    },
    quantity: {
        display: 'flex',
        alignItems: 'center',
    },
    toolbar: {
        display: 'flex',
        justifyContent: 'space-between',
        alignItems: 'center',
        padding: theme.spacing(2),
    },
    leftToolbar: {
        display: 'flex',
        alignItems: 'center',
        gap: theme.spacing(2),
    },
    filterInput: {
        [theme.breakpoints.up('sm')]: {
            width: 300,
        },
    },
}));

const ProductList = () => {
    const classes = useStyles();
    const navigate = useNavigate();
    const [products, setProducts] = useState([]);
    const [cart, setCart] = useState({});
    const [error, setError] = useState('');
    const [filter, setFilter] = useState('');

    useEffect(() => {
        const fetchProducts = async () => {
            try {
                const response = await axios.get('http://localhost:5200/api/Products/');
                setProducts(response.data);
            } catch (error) {
                setError('Failed to fetch products');
            }
        };
        fetchProducts();
    }, []);

    const addToCart = (productId) => {
        setCart((currentCart) => {
            const newQuantity = (currentCart[productId] || 0) + 1;
            return { ...currentCart, [productId]: newQuantity };
        });
    };

    const removeFromCart = (productId) => {
        setCart((currentCart) => {
            const newQuantity = (currentCart[productId] || 0) - 1;
            if (newQuantity <= 0) {
                const updatedCart = { ...currentCart };
                delete updatedCart[productId];
                return updatedCart;
            }
            return { ...currentCart, [productId]: newQuantity };
        });
    };

    const handleFilterChange = (event) => {
        setFilter(event.target.value.toLowerCase());
    };

    const filteredProducts = products.filter((product) =>
        product.name.toLowerCase().includes(filter)
    );

    if (error) {
        return <Typography color="error">{`Error: ${error}`}</Typography>;
    }

    return (
        <>
            <div className={classes.toolbar}>
                <div className={classes.leftToolbar}>
                    <IconButton onClick={() => navigate('/')} color="inherit">
                        <ArrowBackIcon />
                    </IconButton>
                    <TextField
                        className={classes.filterInput}
                        label="Filter Products"
                        variant="outlined"
                        onChange={handleFilterChange}
                    />
                </div>
                <Badge badgeContent={Object.values(cart).reduce((acc, curr) => acc + curr, 0)} color="secondary">
                    <ShoppingCartIcon />
                </Badge>
            </div>
            <TableContainer component={Paper} className={classes.tableContainer}>
                <Typography variant="h4" className={classes.title}>
                    Products
                </Typography>
                <Table className={classes.table} aria-label="simple table">
                    <TableHead>
                        <TableRow>
                            <TableCell className={classes.tableHeaderCell}>Product ID</TableCell>
                            <TableCell className={classes.tableHeaderCell}>Name</TableCell>
                            <TableCell className={classes.tableHeaderCell}>Product Number</TableCell>
                            <TableCell className={classes.tableHeaderCell}>Quantity</TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {filteredProducts.map((product) => (
                            <TableRow key={product.productId}>
                                <TableCell component="th" scope="row">
                                    {product.productId}
                                </TableCell>
                                <TableCell>{product.name}</TableCell>
                                <TableCell>{product.productNumber}</TableCell>
                                <TableCell className={classes.quantity}>
                                    <IconButton onClick={() => removeFromCart(product.productId)} size="small">
                                        <RemoveIcon />
                                    </IconButton>
                                    {cart[product.productId] || 0}
                                    <IconButton onClick={() => addToCart(product.productId)} size="small">
                                        <AddIcon />
                                    </IconButton>
                                </TableCell>
                            </TableRow>
                        ))}
                    </TableBody>
                </Table>
            </TableContainer>
        </>
    );
};

export default ProductList;
