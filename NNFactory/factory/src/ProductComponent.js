import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { makeStyles } from '@material-ui/core/styles';
import {
    Table,
    TableBody,
    TableCell,
    TableContainer,
    TableHead,
    TableRow,
    Paper,
    Typography,
    IconButton,
    Badge
} from '@material-ui/core';
import AddIcon from '@material-ui/icons/Add';
import RemoveIcon from '@material-ui/icons/Remove';
import ShoppingCartIcon from '@material-ui/icons/ShoppingCart';

const useStyles = makeStyles(theme => ({
    table: {
        minWidth: 650,
    },
    tableContainer: {
        borderRadius: 15,
        margin: '10px 10px',
        maxWidth: 950
    },
    tableHeaderCell: {
        fontWeight: 'bold',
        backgroundColor: theme.palette.primary.dark,
        color: theme.palette.getContrastText(theme.palette.primary.dark)
    },
    title: {
        padding: '16px',
        color: theme.palette.primary.dark
    },
    quantity: {
        display: 'flex',
        alignItems: 'center',
    },
    cartIcon: {
        position: 'absolute',
        top: theme.spacing(2),
        right: theme.spacing(2),
    }
}));

const ProductList = () => {
    const classes = useStyles();
    const [products, setProducts] = useState([]);
    const [cart, setCart] = useState({});
    const [error, setError] = useState(null);

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

    const addToCart = productId => {
        setCart(currentCart => {
            const newQuantity = (currentCart[productId] || 0) + 1;
            return { ...currentCart, [productId]: newQuantity };
        });
    };

    const removeFromCart = productId => {
        setCart(currentCart => {
            const newQuantity = (currentCart[productId] || 0) - 1;
            if (newQuantity <= 0) {
                const updatedCart = { ...currentCart };
                delete updatedCart[productId];
                return updatedCart;
            }
            return { ...currentCart, [productId]: newQuantity };
        });
    };

    const getCartTotal = () => {
        return Object.values(cart).reduce((acc, curr) => acc + curr, 0);
    };

    if (error) {
        return <Typography color="error">{`Error: ${error}`}</Typography>;
    }

    return (
        <>
            <Badge badgeContent={getCartTotal()} color="secondary" className={classes.cartIcon}>
                <ShoppingCartIcon />
            </Badge>
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
                            <TableCell className={classes.tableHeaderCell}>Actions</TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {products.map(product => (
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
